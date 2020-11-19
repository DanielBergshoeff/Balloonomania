using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public GameObject MenuPanel;
    public GameObject MainCamera;
    Vector3 pos;
    Vector3 CameraPauzeStartPosition;
    Vector3 CameraPauzeMenuPosition;
    Vector3 CameraMainMenuPosition;
    bool zoomingOut = false;
    bool zoomingIn = false;
    bool movingUp = false;
    bool movingDown = false;
    private bool paused = false;

    float timeElapsed;
    public float lerpDuration = 3;


    public float zoomingAmount;
    public float moveAmount;




    void Update()
    {

        if (!paused)
        {
            pos.x = MainCamera.transform.position.x;
            pos.y = MainCamera.transform.position.y;
            transform.position = pos;


       
        }
        else
        {
            if (zoomingOut)
            {
                float newCamPosition;
                if (timeElapsed < lerpDuration)
                {
                    newCamPosition = Mathf.Lerp(CameraPauzeStartPosition.z, CameraPauzeMenuPosition.z, timeElapsed / lerpDuration);
                    timeElapsed += Time.unscaledDeltaTime;
                }
                else
                {
                    newCamPosition = CameraPauzeMenuPosition.z;
                    zoomingOut = false;
                }
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, newCamPosition);
            }

            if(zoomingIn)
            {
                float newCamPosition;
                if (timeElapsed < lerpDuration)
                {
                    newCamPosition = Mathf.Lerp(CameraPauzeMenuPosition.z, CameraPauzeStartPosition.z, timeElapsed / lerpDuration);
                    timeElapsed += Time.unscaledDeltaTime;
                }
                else
                {
                    newCamPosition = CameraPauzeStartPosition.z;
                    zoomingIn = false;
                    paused = false;
                    MenuPanel.SetActive(false);
                }
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, newCamPosition);

            }

            if (movingDown)
            {
                float newCamPosition;
                if (timeElapsed < lerpDuration)
                {
                    newCamPosition = Mathf.Lerp(CameraPauzeMenuPosition.y, CameraMainMenuPosition.y, timeElapsed / lerpDuration);
                    timeElapsed += Time.unscaledDeltaTime;
                }
                else
                {
                    newCamPosition = CameraMainMenuPosition.y;
                    movingDown = false;
                }
                MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, newCamPosition, MainCamera.transform.position.z);

            }

            if (movingUp)
            {
                float newCamPosition;
                if (timeElapsed < lerpDuration)
                {
                    newCamPosition = Mathf.Lerp(CameraMainMenuPosition.y, CameraPauzeMenuPosition.y, timeElapsed / lerpDuration);
                    timeElapsed += Time.unscaledDeltaTime;
                    MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, newCamPosition, MainCamera.transform.position.z);
                }
                else
                {
                    newCamPosition = CameraMainMenuPosition.y;
                    movingUp = false;
                    ResumeGame();
                }

            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                PauzeGame();
            else
                ResumeGame();
        }
    }

    public void PauzeGame()
    {
        Time.timeScale = 0f;
        timeElapsed = 0f;
        MenuPanel.SetActive(true);
        paused = true;
        CameraPauzeStartPosition = MainCamera.transform.position;
        CameraPauzeMenuPosition = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y, MainCamera.transform.position.z - zoomingAmount);
        zoomingOut = true;
       
    }

    public void ResumeGame()
    {
        timeElapsed = 0f;
        Time.timeScale = 1f;
        zoomingIn = true;
    }

    public void ToMainMenu()
    {
        timeElapsed = 0f;
        
        CameraMainMenuPosition = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y - moveAmount, MainCamera.transform.position.z);
        movingDown = true;
    }

    public void QuitGame()
    {
        AppHelper.Quit();
    }

    public void PlayGame()  
    {
        timeElapsed = 0f;
        movingUp = true;
        
    }


}
