from collections import Counter
from functools import reduce

class ReportGenerator:
    @staticmethod
    def daily_summary(tickets):
        # ADVANCED PYTHON: Filter & Reduce[cite: 1]
        open_tkts = list(filter(lambda t: t['status'] == 'Open', tickets))
        closed_tkts = list(filter(lambda t: t['status'] == 'Closed', tickets))
        breached = list(filter(lambda t: t['status'] == 'SLA Breached', tickets))
        
        high_pri = reduce(lambda acc, t: acc + 1 if t['priority'] in ['P1', 'P2'] else acc, tickets, 0)

        print("\n=== Daily Summary Report ===")
        print(f"- Total Tickets Raised: {len(tickets)}")
        print(f"- Open Tickets: {len(open_tkts)}")
        print(f"- Closed Tickets: {len(closed_tkts)}")
        print(f"- High Priority (P1/P2): {high_pri}")
        print(f"- SLA Breached: {len(breached)}")

    @staticmethod
    def monthly_trend(tickets):
        if not tickets:
            print("No data available for trend report.")
            return
            
        # ADVANCED PYTHON: Map[cite: 1]
        issues = list(map(lambda t: t.get('issue_type', 'Unknown'), tickets))
        depts = list(map(lambda t: t.get('dept', 'Unknown'), tickets))
        
        most_common_issue = Counter(issues).most_common(1)[0][0] if issues else "N/A"
        dept_most_incidents = Counter(depts).most_common(1)[0][0] if depts else "N/A"
        
        print("\n=== Monthly Trend Report ===")
        print(f"- Most Common Issue: {most_common_issue}")
        print(f"- Department w/ Most Incidents: {dept_most_incidents}")