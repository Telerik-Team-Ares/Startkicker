# Startkicker
---

## Team Members
* Pavel_Dobranov - Павел Добранов
* Stev3n - Стивън Цветков
* Goran91 - Горан Цветков
* velimira.madjarova - Велимира Маджарова
* DanteSparda - Владимир Илиев

## Startkicker Description
Startkicker is a web application that provide services for publishing and funding a projects.The idea is to help bring creative projects to life.All users have rights to create projects and fund other projects. Project creators choose a deadline and a minimum funding goal and donators choose which projects they will fund.

## RESTful API Overview
| HTTP Method | Web service endpoint | Description |
|:----------:|:-----------:|:-------------|
|POST (public) | api/account/register | Registers a new user in the system |
|POST (public) | token | Get token for login|
|GET (public)|api/categories|Get all categories|
|GET (public)|api/categories/{id}|Get category by id|
|POST|api/categories|Create new category and return the category id|
|Delete|api/categories/{id}|Remove category|
|POST|api/images|Upload new image for existing project|
|DELETE|api/images|Remove image|
|GET (public)|api/projects/{id}| Get project with given id|
|GET (public)|api/projects?page=n|  Gets (page-1)*10 -> (page-1)*10+10 projects|
|GET (public)|api/projects?categoryId=n&page=k| Gets (page-1)*10 -> (page-1)*10+10 projects from category with id k|
|POST|api/projects| Create new project and return project id|
|PUT|api/projects| Check if the user have available money to fund the project, if yes update collected money for the given porject and update the available money for the user, if no enough money inform the user.|
|DELETE|api/project/ Remove project|
|GET|api/users/{id}| Get user by id|

## GitHub repository
https://github.com/Telerik-Team-Ares/Startkicker.git
