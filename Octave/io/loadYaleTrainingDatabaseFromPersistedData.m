function [data, labels] = loadYaleTrainingDatabaseFromPersistedData()
  % loadYaleTrainingDatabaseFromPersistedData - load the Yale training database from persisted data
  %
  % Outputs:
  %    data - the training database faces
  %    labels - the training database labels

  % Author: Darren Keane
  % Institute of Technology, Tralee
  % email: darren.m.keane@students.ittralee.ie
  
  imagesFileName = "yale_database_images";
  labelsFileName = "yale_database_labels";

  data = loadMatrixData(imagesFileName);
  labels = loadMatrixData(labelsFileName);
endfunction