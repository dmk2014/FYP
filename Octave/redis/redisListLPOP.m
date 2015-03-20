function value = redisListLPOP(redisConnection, redisKey)
  if (nargin != 2)
    usage("redisListLPOP(R, key)");
  endif
  
  #Left pop an item from the lsit specified by redisKey
  redisCommand(redisConnection,"LPOP",redisKey);
  
  #Parse the response using the Redis protocol
endfunction