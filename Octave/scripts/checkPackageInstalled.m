function result = checkPackageInstalled(pkgName)
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