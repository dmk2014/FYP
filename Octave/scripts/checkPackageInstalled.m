function result = checkPackageInstalled(pkgName)
  % checkPackageInstalled - checks if the specified package is already installed
  %
  % Inputs:
  %    pkgName - the name of the package
  %
  % Outputs:
  %    result - boolean indicating if the package is installed

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  if(nargin != 1)
    usage("checkPackageInstalled(pkgName)");
  endif
  
  packages = pkg ("list");
  numPackages = numel(packages);

  for i=1:numPackages
    if packages{1,i}.name == pkgName;
      result = true;
      return
    endif
  endfor
  
  result = false;
endfunction