% testUtil - This script will execute tests on Util functions

% Author: Darren Keane
% Institute of Technology, Tralee
% email: darren.m.keane@students.ittralee.ie


% ___Test Normalise___

%!test
%! testMatrix = [500, 100, 256, 0];
%! 
%! result = normalize(testMatrix, 0, 255);
%!
%! for i=columns(testMatrix)
%!  assert(testMatrix(:, i) >= 0 && testMatrix(:, i) <= 255);
%! endfor