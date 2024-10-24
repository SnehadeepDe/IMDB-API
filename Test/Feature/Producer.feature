Feature: Producer Resource

Scenario: Get All Producers
    Given I am a client
    When I make GET Request '/producers'
    Then response code must be '200'
    And response data must look like '[{"id":1,"name":"Producer 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"},{"id":2,"name":"Producer 2","bio":"Bio 2","dob":"2000-05-05T00:00:00","gender":"Female"}]'

Scenario Outline: Get Producer by ID
    Given I am a client
    When I make GET Request '/producers/<producerId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | producerId | statusCode | expectedResponse                                                                       |
      | 1          | 200        | {"id":1,"name":"Producer 1","bio":"Bio 1","dob":"2000-05-05T00:00:00","gender":"Male"} |
      | 999        | 404        | {"message":"Producer not found"}                                                       |

Scenario Outline: Create Producer
    Given I am a client
    When I am making a POST request to '/producers' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | postDataJson                                                           | statusCode | expectedResponse                              |
      | {"name":"Producer 3","bio":"Bio 3","dob":"2000-05-05","gender":"Male"} | 201        | {"id":3}                                      |
      | {"name":"","bio":"Bio 3","dob":"2000-01-01","gender":"Male"}           | 400        | {"message":"Producer name is required."}      |
      | {"name":"Producer 3","bio":"Bio 3","dob":"2000-50-50","gender":"Male"} | 400        | {"message":"Invalid date of birth provided."} |
      | {"name":"Producer 3","bio":"","dob":"2000-05-05","gender":"Male"}      | 400        | {"message":"Bio is required"}                 |
      | {"name":"Producer 3","bio":"Bio 3","dob":"2000-05-05","gender":""}     | 400        | {"message":"Gender is required"}              |

Scenario Outline: Update Producer
    Given I am a client
    When I make PUT Request '/producers/<producerId>' with the following Data '<postDataJson>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | producerId | postDataJson                                                                 | statusCode | expectedResponse                              |
      | 1          | {"name":"Updated Producer","bio":"Bio 1","dob":"2000-05-05","gender":"Male"} | 200        |                                               |
      | 1          | {"name":"","bio":"Bio 1","dob":"2000-01-01","gender":"Male"}                 | 400        | {"message":"Producer name is required."}      |
      | 1          | {"name":"Updated Producer","bio":"Bio 1","dob":"2000-54-54","gender":"Male"} | 400        | {"message":"Invalid date of birth provided."} |
      | 999        | {"name":"Updated Producer","bio":"Bio 1","dob":"2000-05-05","gender":"Male"} | 404        | {"message":"Producer not found"}              |

Scenario Outline: Delete Producer
    Given I am a client
    When I make DELETE Request '/producers/<producerId>'
    Then response code must be '<statusCode>'
    And response data must look like '<expectedResponse>'

    Examples:
      | producerId | statusCode | expectedResponse                         |
      | 1          | 200        | {"message":"Producer with Id 1 deleted"} |
      | 999        | 404        | {"message":"Producer not found"}         |
