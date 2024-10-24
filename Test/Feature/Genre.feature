Feature: Genre Resource

Scenario: Get All Genres
    Given I am a client
    When I make GET Request '/genres'
    Then response code must be '200'
    And response data must look like '[{"id":1,"name":"Genre 1"},{"id":2,"name":"Genre 2"}]'

Scenario Outline: Get Genre by ID
    Given I am a client
    When I make GET Request '/genres/<genreId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | genreId | statusCode | expectedResponse              |
      | 1       | 200        | {"id":1,"name":"Genre 1"}     |
      | 999     | 404        | {"message":"Genre not found"} |

Scenario Outline: Create Genre
    Given I am a client
    When I am making a POST request to '/genres' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | postDataJson      | statusCode | expectedResponse                      |
      | {"name": "Test3"} | 201        | {"id":3}                              |
      | {"name": ""}      | 400        | {"message":"Genre name is required."} |

Scenario Outline: Update Genre
    Given I am a client
    When I make PUT Request '/genres/<genreId>' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | genreId | postDataJson              | statusCode | expectedResponse                      |
      | 1       | {"name": "Updated genre"} | 200        |                                       |
      | 1       | {"name": ""}              | 400        | {"message":"Genre name is required."} |
      | 999     | {"name": "Updated genre"} | 404        | {"message":"Genre not found"}         |

Scenario Outline: Delete Genre
    Given I am a client
    When I make DELETE Request '/genres/<genreId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

     Examples:
      | genreId | statusCode | expectedResponse                      |
      | 1       | 200        | {"message":"Genre with Id 1 deleted"} |
      | 999     | 404        | {"message":"Genre not found"}         |
