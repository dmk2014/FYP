function redisSendResponse(R, code, data)
  % redisSendResponse - send a response message to Redis
  %
  % Inputs:
  %    R - the redisConnection to which the response will be sent
  %    code - the response code
  %    data - the response data

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if (nargin != 3)
    usage("redisSendResponse(R, code, data)");
  endif
 
  % Reference globals that will be used
  global ResponseCodeKey;
  global ResponseDataKey;
  
  % Transactionality is not supported by go-redis, the "EXEC" command will timeout.
  % Therefore, the data key is set before the code key.
  % This prevents an issue where calling clients were retrieving the data key before it had a value.
  redisSet(R, ResponseDataKey, data);
  redisSet(R, ResponseCodeKey, code);
endfunction