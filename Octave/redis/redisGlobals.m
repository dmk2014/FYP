% Global values utilised by the Redis integration
% These values must be loaded on application startup

% Recogniser Request Globals
% Request Keys
global RequestCodeKey = "facial.request.code";
global RequestDataKey = "facial.request.data";

% Request Codes
global NoData = 50;
global RequestRecognition = 100;
global RequestReload = 200;
global RequestSave = 300;
global RequestRetrain = 400;

% Recogniser Response Globals
% Response Keys
global ResponseCodeKey = "facial.response.code";
global ResponseDataKey = "facial.response.data";

% Response Codes
global ResponseOK = 100;
global ResponseFail = 200;

% Recogniser Status Globals
% Status Key
global RecogniserStatusKey = "facial.recogniser.status"

% Status Codes
global RecogniserAvailable = 100;
global RecogniserBusy = 200;