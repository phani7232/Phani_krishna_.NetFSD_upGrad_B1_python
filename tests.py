import unittest
from tickets import TicketManager
from utils import save_data, TicketNotFoundError

class TestSmartServiceDesk(unittest.TestCase):
    def setUp(self):
        save_data([], 'data/tickets.json')
        self.tm = TicketManager([])

    def test_ticket_creation_and_priority(self):
        t_id = self.tm.create_ticket("John Doe", "IT", "Slow", "Hardware", "Laptop Slow")
        ticket = next(t for t in self.tm._tickets if t['ticket_id'] == t_id)
        self.assertEqual(ticket['priority'], 'P3')

    def test_exception_handling(self):
        with self.assertRaises(TicketNotFoundError):
            self.tm.update_status("FAKE_ID", "Closed")

    def test_search_ticket(self):
        t_id = self.tm.create_ticket("Jane", "HR", "Desc", "Network", "Internet Down")
        ticket = self.tm.search_ticket(t_id)
        self.assertIsNotNone(ticket)

if __name__ == "__main__":
    unittest.main()