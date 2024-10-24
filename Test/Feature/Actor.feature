Feature: Actor Resource

Scenario: Get All Actors
    Given I am a client
    When I make GET Request '/actors'
    Then response code must be '200'
    And response data must look like '[{"id":1,"name":"Actor 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"},{"id":2,"name":"Actor 2","bio":"Bio 2","dob":"2000-05-05T00:00:00","gender":"Female"}]'

Scenario Outline: Get Actor by ID
    Given I am a client
    When I make GET Request '/actors/<actorId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | actorId | statusCode | expectedResponse                                                                    |
      | 1       | 200        | {"id":1,"name":"Actor 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"} |
      | 999     | 404        | {"message":"Actor not found"}                                                       |

Scenario Outline: Create Actor
    Given I am a client
    When I am making a POST request to '/actors' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | postDataJson                                                        | statusCode | expectedResponse                              |
      | {"name":"Actor 3","bio":"Bio 3","dob":"2000-05-05","gender":"Male"} | 201        | {"id":3}                                      |
      | {"name":"","bio":"Bio 3","dob":"2000-01-01","gender":"Male"}        | 400        | {"message":"Actor name is required."}         |
      | {"name":"Actor 3","bio":"Bio 3","dob":"2000-50-50","gender":"Male"} | 400        | {"message":"Invalid date of birth provided."} |
      | {"name":"Actor 3","bio":"","dob":"2000-5-5","gender":"Male"}        | 400        | {"message":"Bio is required"}                 |
      | {"name":"Actor 3","bio":"Bio 3","dob":"2000-5-5","gender":""}       | 400        | {"message":"Gender is required"}              |

Scenario Outline: Update Actor
    Given I am a client
    When I make PUT Request '/actors/<actorId>' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | actorId | postDataJson                                                              | statusCode | expectedResponse                              |
      | 1       | {"name":"Updated Actor","bio":"Bio 1","dob":"2000-05-05","gender":"Male"} | 200        |                                               |
      | 1       | {"name":"","bio":"Bio 1","dob":"2000-01-01","gender":"Male"}              | 400        | {"message":"Actor name is required."}         |
      | 1       | {"name":"Updated Actor","bio":"Bio 1","dob":"2000-54-54","gender":"Male"} | 400        | {"message":"Invalid date of birth provided."} |
      | 999     | {"name":"Updated Actor","bio":"Bio 1","dob":"2000-05-05","gender":"Male"} | 404        | {"message":"Actor not found"}                 |
      | 1       | {"name":"Updated Actor","bio":"","dob":"2000-05-05","gender":"Male"}      | 400        | {"message":"Bio is required"}                 |
      | 1       | {"name":"Updated Actor","bio":"Bio 1","dob":"2000-05-05","gender":""}     | 400        | {"message":"Gender is required"}              |

Scenario Outline: Delete Actor
    Given I am a client
    When I make DELETE Request '/actors/<actorId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | actorId | statusCode | expectedResponse                      |
      | 1       | 200        | {"message":"Actor with Id 1 deleted"} |
      | 999     | 404        | {"message":"Actor not found"}         |
