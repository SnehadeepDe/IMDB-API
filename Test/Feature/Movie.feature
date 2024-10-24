Feature: Movie Resource

Scenario: Get All Movies
    Given I am a client
    When I make GET Request '/movies'
    Then response code must be '200'
    And response data must look like '[{"id":1,"name":"Movie 1","yearOfRelease":2000,"plot":"Plot 1","coverImage":"image1.jpg","producer":{"id":1,"name":"Producer 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"},"actors":[{"id":1,"name":"Actor 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"},{"id":2,"name":"Actor 2","bio":"Bio 2","dob":"2000-05-05T00:00:00","gender":"Female"}],"genres":[{"id":1,"name":"Genre 1"},{"id":2,"name":"Genre 2"}],"reviews":[{"id":1,"message":"Review 1","movieId":1}]},{"id":2,"name":"Movie 2","yearOfRelease":2000,"plot":"Plot 2","coverImage":"image2.jpg","producer":{"id":2,"name":"Producer 2","bio":"Bio 2","dob":"2000-05-05T00:00:00","gender":"Female"},"actors":[{"id":2,"name":"Actor 2","bio":"Bio 2","dob":"2000-05-05T00:00:00","gender":"Female"}],"genres":[{"id":2,"name":"Genre 2"}],"reviews":[{"id":2,"message":"Review 2","movieId":2}]}]'

Scenario Outline: Get Movie by ID
    Given I am a client
    When I make GET Request '/movies/<movieId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | movieId | statusCode | expectedResponse                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
      | 1       | 200        | {"id":1,"name":"Movie 1","yearOfRelease":2000,"plot":"Plot 1","coverImage":"image1.jpg","producer":{"id":1,"name":"Producer 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"},"actors":[{"id":1,"name":"Actor 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"},{"id":2,"name":"Actor 2","bio":"Bio 2","dob":"2000-05-05T00:00:00","gender":"Female"}],"genres":[{"id":1,"name":"Genre 1"},{"id":2,"name":"Genre 2"}],"reviews":[{"id":1,"message":"Review 1","movieId":1}]} |
      | 999     | 404        | {"message":"Movie not found"}                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |

Scenario Outline: Create Movie
    Given I am a client
    When I am making a POST request to '/movies' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | postDataJson                                                                                                                         | statusCode | expectedResponse                          |
      | {"name":"Movie 3","yearOfRelease":2023,"plot":"Plot 3","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"}   | 201        | {"id":3}                                  |
      | {"name":"","yearOfRelease":2023,"plot":"Plot 3","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"}          | 400        | {"message":"Movie name is required."}     |
      | {"name":"Movie 3","yearOfRelease":1555,"plot":"Plot 3","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"}   | 400        | {"message":"Year of release is invalid."} |
      | {"name":"Movie 3","yearOfRelease":2010,"plot":"","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"}         | 400        | {"message":"Movie plot is required."}     |
      | {"name":"Movie 3","yearOfRelease":2010,"plot":"Plot 3","coverImage":"","producerId":1,"actorIds":"1,2","genreIds":"1,2"}             | 400        | {"message":"Cover image is required."}    |
      | {"name":"Movie 3","yearOfRelease":2023,"plot":"Plot 3","coverImage":"image3.jpg","producerId":999,"actorIds":"1,2","genreIds":"1,2"} | 404        | {"message":"Producer not found"}          |
      | {"name":"Movie 3","yearOfRelease":2023,"plot":"Plot 3","coverImage":"image3.jpg","producerId":1,"actorIds":"999,2","genreIds":"1,2"} | 404        | {"message":"Actor not found"}             |
      | {"name":"Movie 3","yearOfRelease":2023,"plot":"Plot 3","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"999,2"} | 404        | {"message":"Genre not found"}             |

Scenario Outline: Update Movie
    Given I am a client
    When I make PUT Request '/movies/<movieId>' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | movieId | postDataJson                                                                                                                                         | statusCode | expectedResponse                          |
      | 1       | {"name":"Updated Movie","yearOfRelease":2023,"plot":"Updated Plot","coverImage":"updatedimage.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"} | 200        |                                           |
      | 999     | {"name":"Updated Movie","yearOfRelease":2023,"plot":"Updated Plot","coverImage":"updatedimage.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"} | 404        | {"message":"Movie not found"}             |
      | 1       | {"name":"","yearOfRelease":2023,"plot":"Updated Plot","coverImage":"updatedimage.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"}              | 400        | {"message":"Movie name is required."}     |
      | 1       | {"name":"Updated Movie","yearOfRelease":2010,"plot":"","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"}                   | 400        | {"message":"Movie plot is required."}     |
      | 1       | {"name":"Updated Movie","yearOfRelease":1555,"plot":"Updated Plot","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"1,2"}       | 400        | {"message":"Year of release is invalid."} |
      | 1       | {"name":"Updated Movie","yearOfRelease":2010,"plot":"Updated Plot","coverImage":"","producerId":1,"actorIds":"1,2","genreIds":"1,2"}                 | 400        | {"message":"Cover image is required."}    |
      | 1       | {"name":"Updated Movie","yearOfRelease":2023,"plot":"Updated Plot","coverImage":"image3.jpg","producerId":999,"actorIds":"1,2","genreIds":"1,2"}     | 404        | {"message":"Producer not found"}          |
      | 1       | {"name":"Updated Movie","yearOfRelease":2023,"plot":"Updated Plot","coverImage":"image3.jpg","producerId":1,"actorIds":"999,2","genreIds":"1,2"}     | 404        | {"message":"Actor not found"}             |
      | 1       | {"name":"Updated Movie","yearOfRelease":2023,"plot":"Updated Plot","coverImage":"image3.jpg","producerId":1,"actorIds":"1,2","genreIds":"999,2"}     | 404        | {"message":"Genre not found"}             |

Scenario Outline: Delete Movie
    Given I am a client
    When I make DELETE Request '/movies/<movieId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | movieId | statusCode | expectedResponse                      |
      | 1       | 200        | {"message":"Movie with Id 1 deleted"} |
      | 999     | 404        | {"message":"Movie not found"}         |
