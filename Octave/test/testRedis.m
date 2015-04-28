% testRedis - This script will execute tests on Redis functions
%             Ensure Redis is running on localhost and default port - 6379

% Author: Darren Keane
% Institute of Technology, Tralee
% email: darren.m.keane@students.ittralee.ie


% ___Test Redis Connection Successful___

%!test
%! host = "127.0.0.1";
%! port = 6379;
%! conn = redisConnection(host, port);
%!
%! pong = redisPing(conn);
%! 
%! % Expected ping result is a 'pong' in Redis protocol format
%! expectedResult = "+PONG\r\n";
%! assert(pong, expectedResult);


% ___Test Redis Connection Fails___

%!test
%! host = "127.0.0.1";
%! port = 80;
%! functionCall = "redisConnection(host, port)";
%! 
%! fail(functionCall);


% ___Test Redis Set___

%!test
%! host = "127.0.0.1";
%! port = 6379;
%! conn = redisConnection(host, port);
%! 
%! key = "redis_set_test";
%! value = "test_value";
%!
%! % No failures indicate a success
%! redisSet(conn, key, value);


% ___Test Redis Get___

%!test
%! host = "127.0.0.1";
%! port = 6379;
%! conn = redisConnection(host, port);
%! 
%! key = "redis_get_test";
%! value = "test_value";
%!
%! redisSet(conn, key, value);
%! result = redisGet(conn, key);
%!
%! assert(result, value);