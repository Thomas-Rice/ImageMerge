Feature: MergeImageIntoAlbum
	In order to identify what album my image belongs to
	As a consumer of the service
	I want to be shown what album the image is in


Scenario: Merge image into album
	Given I have the following image
	| albumID | id | title                                              | url                            | thumbnailUrl                   |
	| 1       | 1  | accusamus beatae ad facilis cum similique qui sunt | http://placehold.it/600/92c952 | http://placehold.it/150/92c952 |
	And the following Album
	| userId | id | title                                              |
	| 1      | 1  | accusamus beatae ad facilis cum similique qui sunt |
	When When I call the merge operation
	Then the result should be the following album:
	| userId | id | title                                              | thumbnailUrl                   | fullImageUrl                   |
	| 1      | 1  | accusamus beatae ad facilis cum similique qui sunt | http://placehold.it/150/92c952 | http://placehold.it/600/92c952 |
