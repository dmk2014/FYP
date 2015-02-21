function redisSendResponse(R,code,data)
  
  if (nargin < 2)
    error("Reponse code and data required");
  endif
  
  redisSet(R,"facial.response.code",code);
  redisSet(R,"facial.response.data",data);
endfunction