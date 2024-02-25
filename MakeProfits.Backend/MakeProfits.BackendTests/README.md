### MakeProfits.BackendTest

This project ensures that the MakeProfits.Backend runs as it was intended. We ensure it by adding unit tests

** Technical information **

| Library Name | Version |
| -------------- | ------- |
| ``` XUnit ``` | ``` 2.7.0 ``` |
| ``` xunit.runner.console ``` | ``` 2.7.0 ``` |
| ``` xunit.runner.visualstudio ``` | ``` 2.5.7 ``` |

| ``` Microsot.NET.Test.SDK  ``` | ``` 17.9.0 ``` |

** Some important steps to remember **

- The unit-test method should indicate two things
    - The method it was testing
    - The behavior it was testing for
- Testing Only One Concern
    
    Concern was an single end result from an unit of work. It can be a return value, change of state or call to a third party object  
- Always follow AAA pattern

   - Arrange - Prepare the ground for the unit test, create and set neccesary objects 

    - Act - call the method under test and get the actual value
    - Assert  - Checks the actual and expected values and decided whether the test was passed or failed
    
- Use ``` [Theory] ``` to test edge cases
