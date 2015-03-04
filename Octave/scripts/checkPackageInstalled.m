function result = checkPackageInstalled(pkgName)

  packages = pkg ("list");
  numPackages = numel(packages);

  for i=1:numPackages
    if packages{1,i}.name == "instrument-control";
      result = true;
      return
    endif
  endfor
  
  result = false;
endfunction