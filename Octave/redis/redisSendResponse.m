function redisSendResponse(R, code, data)
  if (nargin != 3)
    usage("redisSendResponse(R, code, data)");
  endif
  
  redisSet(R, "facial.response.code", code);
  redisSet(R, "facial.response.data", data);
endfunction