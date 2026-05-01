import logging
import os
from functools import wraps

os.makedirs('data', exist_ok=True)

# Setup Logging
logging.basicConfig(
    filename='data/logs.txt',
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# ADVANCED PYTHON: Decorator for logging actions
def log_action(action_name):
    def decorator(func):
        @wraps(func)
        def wrapper(*args, **kwargs):
            try:
                result = func(*args, **kwargs)
                logger.info(f"Action '{action_name}' executed successfully.")
                return result
            except Exception as e:
                logger.error(f"Error in '{action_name}': {e}")
                raise
        return wrapper
    return decorator