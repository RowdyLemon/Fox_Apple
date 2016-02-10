# Fox_Apple


Workflow (This is for use with git Bash, Im not very familiar with the GUI and I don't like it lolz)

(Setup)
- first install git on your machine if you havn't already (https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)
- Clone the repository to your local machine if you havn't already using this command: git clone https://github.com/RowdyLemon/Fox_Apple.git
	*Create a folder to clone the project to, you can call it Fox_Apple for example
	*Right click in the folder and select 'Git Bash Here'
	*Then type the above command into the console and hit enter

(Adding Changes)
- After altering/adding your changes add them with this command: git add -A
	*This will add all changed files, minus those being ignored by the .gitignore 
- Now commit those changes to your local repository with this command: git commit -m "Your message goes here"
	*Add a useful message that is short description of what you changed"
- Before pushing your commit to the master branch pull down any changes that others may have made with this command: git pull
- resolve any conflicts that may exist, an easy tool to do this is tortoisegit and I recomend installing it
- Now push your changes with this command: git push
- And you are done! pretty easy =)