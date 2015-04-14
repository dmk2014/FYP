function value = redisListLPOP(redisConnection, redisKey)
  % redisListLPOP - Left-Pops an item from a Redis list specified by its key
  %
  % Inputs:
  %    redisConnection - the redisConnection to use
  %    redisKey - the key identifying the list from which to pop an item
  %
  % Outputs:
  %    value - the value popped from the list

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if (nargin != 2)
    usage("redisListLPOP(redisConnection, redisKey)");
  endif
  
  % Left pop an item from the list specified by redisKey
  response = redisCommand(redisConnection, "LPOP", redisKey);
  
  % Parse the response using the Redis protocol
  % Redis Protocol: http://redis.io/topics/protocol
  %
  % LPOP response is a bulk string:
  % Its first line starts with a $ that is followed by the number of bytes
  % composing the string. The endpoint of the first line is marked by a CRLF.
  % This is followed by the actual string data.
  % The response ends with a second CRLF at the end of the string data.
  % e.g. "$6\r\nfoobar\r\n"
  
  % Get the first character of response - this indicates the response type
  responseType = substr(response, 1, 1);
  
  if (responseType == '$')
    responseComponents = strsplit(response, "\r\n");
    
    % responseComponents format:
    % (1,1) = $length
    % (1,2) = string value to be retrieved
    % (1,3) = CRLF
    
    value = responseComponents(1, 2);
    value = cell2mat(value);
  else
    error("Unexpected response type from LPOP command");
  endif
  
endfunction