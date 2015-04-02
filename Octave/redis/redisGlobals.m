# Global values used for parsing and responding to Redis requests
# Values are defined here and used by all functions that require them
# These values must be loaded on application startup

# Recogniser Request Globals
global NO_REQUEST = 50;
global REQUEST_REC = 100;
global REQUEST_RELOAD = 200;
global REQUEST_SAVE = 300;
global REQUEST_RETRAIN = 400;

# Recogniser Response Globals
global RESPONSE_OK = 100;
global RESPONSE_FAIL = 200;

# Recogniser Status Globals
# These are set before Octave executes a request
# Allows calling applications to check if the recogniser is available/busy
global RECOGNISER_AVAILABLE = 100;
global RECOGNISER_BUSY = 200;