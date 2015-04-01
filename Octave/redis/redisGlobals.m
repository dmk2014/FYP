# Global values used for parsing and responding to Redis requests
# Values are defined here and used by all functions that require them
# These values must be loaded on application startup

global NO_REQUEST = 50;
global REQUEST_REC = 100;
global REQUEST_RELOAD = 200;
global REQUEST_SAVE = 300;
global REQUEST_RETRAIN = 400;
global RESPONSE_OK = 100;
global RESPONSE_FAIL = 200;