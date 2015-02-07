result = zeros(32256,1);
w1 = weights(:,1);
sumW = sum(w1); #w1 represents image 1, first col of weight

for i=1:30 
  #multiply and add
  val = sumW * U(:,i);
  
  result = result+val;
endfor

#sum of weight * eigenface



faceToRecon = weights(:,1);
results = [];

for i=1:30
  curResult = faceToRecon * U(:,1)';
  results = [results,curResult];
endfor


faceWeight = weights(:,1);
result = zeros(32256,1);

for i=1:300
  result += faceWeight(i,:) * U(:,i);
endfor