function [data, labels] = loadYaleTrainingDatabaseFromPersistedData()
  imagesFileName = "yale_database_images";
  labelsFileName = "yale_database_labels";

  data = loadMatrixData(imagesFileName);
  labels = loadMatrixData(labelsFileName);
endfunction