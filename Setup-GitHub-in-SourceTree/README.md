# Setup GitHub in SourceTree

If you are not familiar with "Git", the following terminologies may help.
|Term |Meaning                                      |Other name |
|-----|---------------------------------------------|---------  |
|Push |upload local source code to git server       |Check-in   |
|Pull |download source code from git server to local|Check-out  |

# 1	SourceTree: Download & Install
Download SourceTree from online: https://www.sourcetreeapp.com/

Install on Windows: Click “Skip” on the registration page:

![image](https://user-images.githubusercontent.com/41459515/138202056-a5c58dc9-6d6f-43b4-b622-fcdad8329fb2.png)

Only keep “git” checked:

![image](https://user-images.githubusercontent.com/41459515/138202242-108f2374-17b3-41f2-b48c-9729c38d3d3d.png)

Installation should be fast:

![image](https://user-images.githubusercontent.com/41459515/138202278-ae167735-958d-4af5-9a48-36c264effdca.png)


# 2	SourceTree: Clone code from GitHub

Open SourceTree UI, and click the "Clone" menu button:

![image](https://user-images.githubusercontent.com/41459515/138204498-9d674f43-588a-475f-898c-8113de3c4399.png)

Input the GitHub URL and local directory:

|Key              |Value                                                    |
|-----------------|---------------------------------------------------------|
|Source Path/URL  | https://github.com/shifengxu/CE7454_P2.git              |
|Destination path	| Your local folder for the code, such as C:\\dir1\\P2\\  |

Then we get the source code in main branch:

![image](https://user-images.githubusercontent.com/41459515/138204523-4d9f790d-557d-4f16-9725-a5bbdee426e0.png)

Since the git repo (repository) is public, so we can “pull” the code without authentication (username/password). But if we want to “push” new code to git server, we need to setup authentication.

# 3	SourceTree: setup GitHub Authentication

Now we come to the key part of this doc: to setup GitHub authentication in the Source Tree. Before that, we need to generate personal access token in GitHub.

## 3.1	GitHub: Personal access token
Go to Github page, find: Settings

![image](https://user-images.githubusercontent.com/41459515/138204582-acba07b9-67bd-47aa-9122-74477728fbbc.png)

And find developer settings:

![image](https://user-images.githubusercontent.com/41459515/138204607-62ea036d-f3e3-4f04-a3c4-5cdab4f58f35.png)

In the Personal access tokens page, click button “Generate new token”

![image](https://user-images.githubusercontent.com/41459515/138204622-3dab92f5-b8e2-476b-af5c-ed236285f8fb.png)

In the token generation page, please select:

Repo:

![image](https://user-images.githubusercontent.com/41459515/138204640-79f3c8e2-3622-4b74-9083-69d993a09ab6.png)

User:

![image](https://user-images.githubusercontent.com/41459515/138204662-1e165d83-34da-4638-86c9-94b05f3d8cc2.png)

Then click "Generate" button to generate the token. The token will show up only once (and the page will warn you). So we need to copy and save such token for future usage.

## 3.2	SourceTree Auth
Tools -> Options

![image](https://user-images.githubusercontent.com/41459515/138204682-58b7db08-8600-47c2-ae86-ded5edd8e99f.png)

In the Options dialog, click “Authentication” tab.

![image](https://user-images.githubusercontent.com/41459515/138204707-d81d2f3e-812f-4e2b-92da-06fe62953dcf.png)

Hosting Service, select “GitHub”. Authentication, select “Basic”. Then enter username of your GitHub account. After that, click “Refresh password” button. And in the pop-up dialog, enter your GitHub “Personal access token”.

![image](https://user-images.githubusercontent.com/41459515/138204721-699ca10d-008e-49fa-b33a-b5d4a4b499a7.png)

# 4	Commit & push your changes

Simply speaking, we need 2 steps to upload your change from local to git: commit and push. But before that, we need to select our local changes.

## 4.1	Select local change

Firstly, let’s find out the local changes.

Click “File Status” on the top-left “WORKSPACE” panel, it will show you the file status. In the “Unstaged files” panel, you will see the modified files and newly added files and the deleted files. Please note, this is just your local files. Click the file, you will see its changes in the right side.

![image](https://user-images.githubusercontent.com/41459515/138204761-9ad26227-af4c-4bf7-8f48-3aac41eac12d.png)

## 4.2	Commit
From the changed files (including newly added ones and deleted ones), select and only select those you want to commit, and click the “Stage selected” button. Then those files will move to “Staged files” panel.

Put some commit message in the textbox in the bottom, and click “commit” button.

![image](https://user-images.githubusercontent.com/41459515/138204790-3da448d9-cae7-4f30-80be-5329b3c37b00.png)

## 4.3	Push

Click the “Push” menu on the top, and it will pop up the “push” dialog. And click the “Push” button in the dialog.

![image](https://user-images.githubusercontent.com/41459515/138204816-c0f87157-6df8-437b-a63b-ac59d40bb027.png)

If first time to push, it may ask you to select credential helper:

![image](https://user-images.githubusercontent.com/41459515/138204851-148fdf8f-b523-4285-aac3-9da1760596d7.png)

Can just select “wincred” and check “Always use this from now on”.

## 4.4	Windows Credentials

If save GitHub credentials in “wincred”, then we can check and update the credential (password) in Windows Credentials.

![image](https://user-images.githubusercontent.com/41459515/138204874-d28cabb3-27bd-4c23-8e2e-3dc7acd2b63b.png)

 
# 5	Trouble Shoot

## 5.1	Required authentication
In SourceTree, the GitHub password does not work: Required authentication

![image](https://user-images.githubusercontent.com/41459515/138204905-a0e4c2c6-9277-4ee6-9a79-b6ae880755cf.png)

[Solution]:

For the password, we should use GitHub “Personal access token”, not GitHub account password.

## 5.2	user/email was not found

In SourceTree, the GitHub password does not work: user/email was not found

![image](https://user-images.githubusercontent.com/41459515/138204955-4a6b8e25-dc16-4360-a6cc-596cab0adf28.png)

[Solution]:
The GitHub personal access token must have “User” access:

![image](https://user-images.githubusercontent.com/41459515/138204973-8ae87d7e-513a-4351-8861-8049bf8b8ad1.png)
 

