Feature: MergeImageIntoAlbum
	In order to identify what album my image belongs to
	As a consumer of the service
	I want to be shown what album the image is in

Background: 
	Given Given Configurations Are SetUp

Scenario: Merge image into album
	Given I have the following image
	| albumID | id | title     | url                            | thumbnailUrl                   |
	| 32      | 1  | TestTitle | http://placehold.it/600/92c952 | http://placehold.it/150/92c952 |
	And the following Album
	| userId | id | title                                              |
	| 1      | 32 | accusamus beatae ad facilis cum similique qui sunt |
	When When I call the merge operation asking for 1 page and 1 results
	Then the result should be the following album:
	| UserId | PhotoTitle | ThumbnailUrl                   | FullImageUrl                   | AlbumTitle | AlbumId | PhotoId |
	| 1      | TestTitle  | http://placehold.it/150/92c952 | http://placehold.it/600/92c952 | TestTitle  | 32      | 1       |
