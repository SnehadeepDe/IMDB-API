Feature: Review Resource

Scenario Outline: Get All Reviews
    Given I am a client
    When I make GET Request '/movies/<movieId>/reviews'
    Then response code must be '200'
    And response data must look like '<expectedResponse>'

    Examples:
       | movieId | expectedResponse                            |
       | 1       | [{"id":1,"message":"Review 1","movieId":1}] |
       | 2       | [{"id":2,"message":"Review 2","movieId":2}] |

Scenario Outline: Get Review by ID
    Given I am a client
    When I make GET Request '/movies/<movieId>/reviews/<reviewId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | reviewId | movieId | statusCode | expectedResponse                                              |
      | 1        | 1       | 200        | {"id":1,"message":"Review 1","movieId":1}                     |
      | 999      | 1       | 404        | {"message":"Review not found"}                                |
      | 2        | 1       | 404        | {"message":"No review with Review Id = 2 under Movie Id = 1"} |
      | 1        | 999     | 404        | {"message":"Movie not found"}                                 |

Scenario Outline: Create Review
    Given I am a client
    When I am making a POST request to '/movies/<movieId>/reviews' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | movieId | postDataJson           | statusCode | expectedResponse                          |
      | 1       | {"message":"Review 3"} | 201        | {"id":3}                                  |
      | 1       | {"message":""}         | 400        | {"message":"Review message is required."} |
      | 999     | {"message":"Review 3"} | 400        | {"message":"Movie not found."}            |

Scenario Outline: Update Review
    Given I am a client
    When I make PUT Request '/reviews/<reviewId>' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | reviewId | postDataJson                                | statusCode | expectedResponse                          |
      | 1        | {"message":"Update Review","movieId":"1"}   | 200        |                                           |
      | 1        | {"message":"","movieId":"1"}                | 400        | {"message":"Review message is required."} |
      | 999      | {"message":"Update Review","movieId":"1"}   | 404        | {"message":"Review not found"}            |
      | 1        | {"message":"Update Review","movieId":"999"} | 400        | {"message":"Movie not found."}            |

Scenario Outline: Delete Review
    Given I am a client
    When I make DELETE Request '/reviews/<reviewId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | reviewId | statusCode | expectedResponse                       |
      | 1        | 200        | {"message":"Review with Id 1 deleted"} |
      | 999      | 404        | {"message":"Review not found"}         |
