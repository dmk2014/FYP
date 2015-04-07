function redisSendResponse(R, code, data)
  if (nargin != 3)
    usage("redisSendResponse(R, code, data)");
  endif
  % Sends a response to Redis
  
  % Transactionality is not supported by go-redis, the "EXEC" command will timeout.
  % Therefore, the data key is set before the code key.
  % This prevents an issue where calling clients were retrieving the data key before it had a value.
  redisSet(R, "facial.response.data", data);
  redisSet(R, "facial.response.code", code);
endfunction