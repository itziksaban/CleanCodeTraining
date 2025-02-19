## This is the structure of the uploaded file:
  ```
  [
	   {
		   "mac": ...
		   "installedPrograms" : 
		   [
			   {
				   "vendor": ...
				   "version": ...
				   "name": ...
			   },
			   ....
		   ]
	   },
     ....
  ]
  ```

## This is the structure of a device in cosmosDB:
  ```
  {
	   "id": ...
	   "mac": ...
	   "installedPrograms" : 
	   [
		   {
			   "vendor": ...
			   "version": ...
			   "name": ...
			   "firstFound": ...
		   },
		   ....
	   ]
  }
 ```


Logic:
- Quit if Blob is not safe (MalwareScanMessage.IsSafe)
- Quit if blob properties["etag"] != MalwareScanMessage.Etag
- Deserialize the blob content into List of "Scan" objects (involves opening a stream to the blob, downloading the content and deserializing it)
- Iterate the Scans and rest for 1 sec after every 1000 iterations
- While iterating, fetch from the db the corresponding Device of each Scan by comparing the mac address
- For each Device, update its list of InstalledPrograms, taken from the Scan object: remove, add and update
- Save back the device

