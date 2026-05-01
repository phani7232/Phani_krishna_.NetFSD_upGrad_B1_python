
from datetime import datetime, timedelta
import logging
from utils import save_data, backup_to_csv, load_data, TicketNotFoundError
from logger import log_action
from itil import IncidentTicket, ProblemRecord, SLA_HOURS
class TicketManager:
    def __init__(self, initial_data):
        self._tickets = initial_data 
        self._problems = load_data('data/problems.json') # ITIL: Separate problems file[cite: 1]

    def get_all_tickets(self):
        return self._tickets

    # ADVANCED PYTHON: Generator[cite: 1]
    def generate_open_tickets(self):
        for t in self._tickets:
            if t['status'] == 'Open':
                yield t

    @log_action("Create Ticket") # Decorator applied
    def create_ticket(self, emp_name, dept, issue_desc, category, issue_type, t_class=IncidentTicket):
        ticket = t_class(emp_name, dept, issue_desc, category, issue_type)
        self._tickets.append(ticket.to_dict())
        self._save_and_log(f"Ticket created: {ticket.ticket_id}")
        self.check_problem_record(issue_type)
        return ticket.ticket_id

    def check_problem_record(self, issue_type):
        count = sum(1 for t in self._tickets if t.get('issue_type') == issue_type)
        if count % 5 == 0 and count > 0:
            problem = ProblemRecord(f"Repeated Issue: {issue_type} has occurred {count} times.")
            self._problems.append(problem.to_dict())
            save_data(self._problems, 'data/problems.json')
            logging.warning(f"Problem Record automatically created: {problem.ticket_id}")

    @log_action("Update Ticket")
    def update_status(self, ticket_id, status):
        for t in self._tickets:
            if t['ticket_id'] == ticket_id:
                t['status'] = status
                self._save_and_log(f"Ticket {ticket_id} updated to {status}")
                return True
        raise TicketNotFoundError("Wrong ticket ID entered.")

    def delete_ticket(self, ticket_id):
        initial_length = len(self._tickets)
        self._tickets = [t for t in self._tickets if t['ticket_id'] != ticket_id]
        if len(self._tickets) < initial_length:
            self._save_and_log(f"Ticket deleted: {ticket_id}")
            return True
        raise TicketNotFoundError("Wrong ticket ID entered.")

    def search_ticket(self, ticket_id):
        for t in self._tickets:
            if t['ticket_id'] == ticket_id: return t
        raise TicketNotFoundError("Ticket not found.")

    def check_sla_breaches(self):
        now = datetime.now()
        for t in self._tickets:
            if t['status'] not in ['Closed', 'SLA Breached']:
                created = datetime.fromisoformat(t['created_at'])
                limit = created + timedelta(hours=SLA_HOURS.get(t['priority'], 24))
                
                if t['priority'] == 'P1' and now > created + timedelta(minutes=30):
                    logging.warning(f"ESCALATION ALERT: P1 Ticket {t['ticket_id']} unresolved!")
                
                if now > limit:
                    t['status'] = 'SLA Breached'
                    self._save_and_log(f"SLA BREACHED for ticket {t['ticket_id']}", level='critical')

    def _save_and_log(self, msg, level='info'):
        save_data(self._tickets)
        backup_to_csv(self._tickets)
        if level == 'critical': logging.critical(msg) # Explicit CRITICAL log[cite: 1]
        else: logging.info(msg)