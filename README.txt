Please, attach the database to your mssql server


base uri = http://localhost:5000/api/

account/login (post)
account/register (post)
discovers/ (get)  return tags list
discovers/{tagName} (get) get discovers feeds by tag
discovers/{tagName}/{sourceId} (get) get concrete feeds by discover
discovers/collections (get) get collections by user
discovers/collections/{collectionName} (get) get discover feeds from collections
discovers/collections (post) create new collection
discovers/collections/{collectionName} (put) add discover feed to collection
discovers/collections (delete) delete collection
managingsourcefeed/tags (post) create new tag
managingsourcefeed/{tagName} (put) add new source feed by tag



Tested using Postman
 