result = zeros(32256,1);
sumW = sum(w1);

for i=1:30 
  #multiply and add
  val = sumW * U(:,i);
  
  result = result+val;
endfor

#sum of weight * eigenface