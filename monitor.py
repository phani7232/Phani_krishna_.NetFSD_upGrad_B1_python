import psutil
from logger import logger # Using the new custom logger

class Monitor:
    def __init__(self, ticket_manager):
        self.tm = ticket_manager

    def check_system_health(self):
        """Monitor CPU, RAM, Disk and generate high-priority tickets."""
        cpu = psutil.cpu_percent(interval=1)
        ram = psutil.virtual_memory().percent
        disk_percent_free = 100 - psutil.disk_usage('/').percent

        alerts = []
        # Monitoring conditions[cite: 1]
        if cpu > 90: alerts.append(f"CPU usage critically high: {cpu}%")
        if ram > 95: alerts.append(f"RAM usage critically high: {ram}%")
        if disk_percent_free < 10: alerts.append(f"Disk free space critically low: {disk_percent_free}%")

        if not alerts:
            print("✅ System is healthy. No thresholds exceeded.")

        for alert in alerts:
            logger.error(f"System Alert: {alert}") # Logs as ERROR level[cite: 1]
            t_id = self.tm.create_ticket(
                emp_name="System Monitor",
                dept="IT Operations",
                issue_desc=f"Automated Alert: {alert}",
                category="Infrastructure",
                issue_type="Server Down"
            )
            print(f"⚠️ Auto-ticket created for System Alert: {alert} (ID: {t_id})")
    def view_system_status(self):
        cpu = psutil.cpu_percent(interval=1)
        ram = psutil.virtual_memory().percent
        
        # Get disk usage in GB
        disk_usage = psutil.disk_usage('/')
        disk_total = disk_usage.total / (1024 ** 3)
        disk_free = disk_usage.free / (1024 ** 3)
        disk_percent_free = 100 - disk_usage.percent

        print("\n" + "="*30)
        print(" 📊 Current System Health")
        print("="*30)
        print(f" CPU Usage:  {cpu}%")
        print(f" RAM Usage:  {ram}%")
        print(f" Disk Free:  {disk_percent_free:.1f}% ({disk_free:.1f} GB available)")
        print("="*30)