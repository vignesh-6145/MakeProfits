# MakeProfit
A single platform for all your investments


### Projects information

#### MakeProfits.Backend

The backend component of the MakeProfits application

How to work locally

 - Clone the repository
 - open MakeProfits.Backend Solution in the visual studio
 - build the application 
 - run the application


### How to Contribute

- when working on a new PBI make sure that you have the latest changes 
- each PBI should have it's own branch which contains it's code changes
  
  ``` git checkout -b <BranchName> ```
- If you already had the branch then use
 
  ``` git checkout <BranchName> ```

- Once you made the changes make sure you commit your changes with simple & concise commit messages

  ``` git commit -m "<WorkItemKey> : <commit message>" ```

- The next step was to push your changes to remote branches but, Before doing them you should get any updates from the remore repository. 

So, In-order to reflect the changes happened in the remote repository to the local repository, you can follow these steps

   -  pull the remote repository

        ``` git pull ```
    
   -  During this process the un committed changes will be stashed and can be retrieved by

        ``` git stash pop ```
    
- if there are no merge conflicts, you can build your solution proceed further 


- When the changes were tested and push them  to the central repository(GitHub)
  
   ``` git push [-u origin <remote branch name>] ```

- Now raise a PR and ask for a review in the ``` Team-1 insider ``` group and wait for the review
