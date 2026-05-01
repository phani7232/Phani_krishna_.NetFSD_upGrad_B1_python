from tickets import TicketManager
from monitor import Monitor
from reports import ReportGenerator
from utils import load_data, validate_name, InvalidInputError, TicketNotFoundError
import logging

def display_menu():
    print("\n" + "="*30)
    print(" Smart IT Service Desk")
    print("="*30)
    print("1. Create Ticket")
    print("2. View Open Tickets") # Changed to use generator
    print("3. Search Ticket by ID")
    print("4. Update Ticket Status")
    print("5. Close Ticket")
    print("6. Delete Ticket")
    print("7. Run System Monitor(Auto-ticketing)") # NEW OPTION
    print("8. View System Health Status") # NEW OPTION
    print("9. Generate Reports")
    print("10. Exit")
    print("="*30)

def main():
    initial_data = load_data()
    tm = TicketManager(initial_data)
    monitor = Monitor(tm)

    while True:
        tm.check_sla_breaches() 
        display_menu()
        choice = input("Select an option (1-9): ")
        
        try:
            if choice == '1':
                emp = validate_name(input("Employee Name: ")) # Regex applied[cite: 1]
                dept = input("Department: ")
                print("Options: Server Down, Internet Down, Laptop Slow, Password Reset")
                issue = input("Issue Type: ")
                desc = input("Description: ")
                
                t_id = tm.create_ticket(emp, dept, desc, "IT Support", issue)
                print(f"✅ Ticket created successfully! ID: {t_id}")
                    
            elif choice == '2':
                # ADVANCED PYTHON: Consuming Generator[cite: 1]
                tickets = list(tm.generate_open_tickets())
                if not tickets: print("No open tickets found.")
                for t in tickets:
                    print(f"[{t['ticket_id']}] {t['issue_type']} | Pri: {t['priority']} | Status: {t['status']}")
                    
            elif choice == '3':
                t_id = input("Enter Ticket ID to search: ")
                t = tm.search_ticket(t_id)
                print(f"\n--- Ticket Details ---\nID: {t['ticket_id']}\nName: {t['emp_name']}\nIssue: {t['issue_desc']}")
                    
            elif choice == '4':
                t_id = input("Enter Ticket ID: ")
                status = input("New Status (Open/In Progress): ")
                tm.update_status(t_id, status)
                print("✅ Status Updated.")
                    
            elif choice == '5':
                t_id = input("Enter Ticket ID to Close: ")
                tm.update_status(t_id, "Closed")
                print("✅ Ticket Closed.")
                    
            elif choice == '6':
                t_id = input("Enter Ticket ID to Delete: ")
                tm.delete_ticket(t_id)
                print("✅ Ticket Deleted.")
                    
            elif choice == '7':
                print("Checking system health thresholds...")
                monitor.check_system_health()
                
            elif choice == '8': # NEW ELIF BLOCK
                print("Fetching live system metrics...")
                monitor.view_system_status()

            elif choice == '9': # UPDATED NUMBER
                ReportGenerator.daily_summary(tm.get_all_tickets())
                ReportGenerator.monthly_trend(tm.get_all_tickets())

            elif choice == '10': # UPDATED NUMBER
                print("Exiting Smart IT Service Desk. Goodbye!")
                break
                
            else:
                print("❌ Invalid menu input. Please enter a number between 1 and 10.")

        except InvalidInputError as e: print(f"❌ Input Error: {e}")
        except TicketNotFoundError as e: print(f"❌ Not Found: {e}")
        except Exception as e:
            logging.error(f"Unexpected application error: {e}")
            print(f"❌ An unexpected error occurred. Check logs.")

if __name__ == "__main__":
    main()