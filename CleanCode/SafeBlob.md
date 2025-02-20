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
- Deserialize the blob content into list of `Scan` objects (involves opening a stream to the blob, downloading the content
  and deserializing it)
- Iterate the `Scan` objects and for each `Scan`, fetch from the db the corresponding Device by comparing the mac address
- For each Device, update its list of InstalledPrograms, taken from the Scan object: remove, add and update
- Save the device in the db
- Rest for 1 sec after every 1000 iterations

