import uuid
from datetime import datetime

# Priority and SLA Mapping
PRIORITY_MAP = {'Server Down': 'P1', 'Internet Down': 'P2', 'Laptop Slow': 'P3', 'Password Reset': 'P4'}
SLA_HOURS = {'P1': 1, 'P2': 4, 'P3': 8, 'P4': 24}

class Ticket:
    """ADVANCED OOP: Base Class with Encapsulation & Special Methods"""
    def __init__(self, emp_name, dept, issue_desc, category, issue_type):
        self._ticket_id = str(uuid.uuid4())[:8] # Encapsulation
        self.emp_name = emp_name
        self.dept = dept
        self.issue_desc = issue_desc
        self.category = category
        self.issue_type = issue_type
        self.priority = PRIORITY_MAP.get(issue_type, 'P4')
        self.status = 'Open'
        self.created_at = datetime.now().isoformat()
        
    @property
    def ticket_id(self):
        return self._ticket_id

    def to_dict(self):
        return {
            "ticket_id": self.ticket_id, "emp_name": self.emp_name, "dept": self.dept,
            "issue_desc": self.issue_desc, "category": self.category,
            "issue_type": self.issue_type, "priority": self.priority,
            "status": self.status, "created_at": self.created_at,
            "ticket_type": self.__class__.__name__
        }

    def __str__(self): # Special Method
        return f"[{self.ticket_id}] {self.issue_type} - {self.status}"

    def handle_workflow(self): # Polymorphism base method[cite: 1]
        return "Standard routing."

# ITIL Concepts Implementation[cite: 1]
class IncidentTicket(Ticket):
    def handle_workflow(self): return "Routing to Incident Response Team."

class ServiceRequest(Ticket):
    def handle_workflow(self): return "Routing to Service Desk."

class ProblemRecord(Ticket):
    def __init__(self, issue_desc):
        super().__init__("System", "IT", issue_desc, "Infrastructure", "Repeated Issue")
        self.priority = 'P1'
    def handle_workflow(self): return "Routing to Problem Management."

class ChangeRequest(Ticket):
    def handle_workflow(self): return "Routing to Change Advisory Board."