import json
import csv
import logging
import os
import re

# Create data directory if it doesn't exist
os.makedirs('data', exist_ok=True)

class TicketNotFoundError(Exception): pass
class InvalidInputError(Exception): pass

# ADVANCED PYTHON: Basic Regex
def validate_name(name):
    if not re.match(r"^[A-Za-z\s]+$", name):
        raise InvalidInputError("Name must contain only letters and spaces.")
    return name

def load_data(filepath='data/tickets.json'):
    try:
        with open(filepath, 'r') as file:
            return json.load(file)
    except FileNotFoundError:
        logging.warning(f"{filepath} not found. Starting fresh.")
        return []
    except json.JSONDecodeError:
        logging.error("JSON decode error. File might be corrupted.")
        return []

def save_data(data, filepath='data/tickets.json'):
    with open(filepath, 'w') as file:
        json.dump(data, file, indent=4)

def backup_to_csv(data, filepath='data/backup.csv'):
    if not data: return
    keys = data[0].keys()
    try:
        with open(filepath, 'w', newline='') as file:
            writer = csv.DictWriter(file, fieldnames=keys)
            writer.writeheader()
            writer.writerows(data)
    except Exception as e:
        logging.error(f"Error during CSV backup: {e}")

#Setup Logging
logging.basicConfig(
    filename='data/logs.txt',
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)

def load_data(filepath='data/tickets.json'):
    """Load JSON data with FileNotFoundError handling."""
    try:
        with open(filepath, 'r') as file:
            return json.load(file)
    except FileNotFoundError:
        logging.warning("tickets.json not found. Starting with an empty database.")
        return []
    except json.JSONDecodeError:
        logging.error("JSON decode error. File might be corrupted.")
        return []

def save_data(data, filepath='data/tickets.json'):
    """Store all ticket data."""
    with open(filepath, 'w') as file:
        json.dump(data, file, indent=4)

def backup_to_csv(data, filepath='data/backup.csv'):
    """Create automatic backup of ticket records."""
    if not data:
        return
    keys = data[0].keys()
    try:
        with open(filepath, 'w', newline='') as file:
            writer = csv.DictWriter(file, fieldnames=keys)
            writer.writeheader()
            writer.writerows(data)
    except Exception as e:
        logging.error(f"Error during CSV backup: {e}")