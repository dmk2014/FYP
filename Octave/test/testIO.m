% testIO - This script will execute tests on IO functions

% Author: Darren Keane
% Institute of Technology, Tralee
% email: darren.m.keane@students.ittralee.ie

% ___Test Load Yale Training Database___

%!test
%! yaleFaces = loadYaleTrainingDatabase();
%! 
%! expectedRows = 32256;
%! expectedColumns = 2432;
%!
%! assert(rows(yaleFaces), expectedRows);
%! assert(columns(yaleFaces), expectedColumns);


% ___Test Load Matrix Data Fails For Non-Existing File___

%!test
%! functionCall = "loadMatrixData(\"this_file_does_not_exist\")";
%! fail(functionCall);