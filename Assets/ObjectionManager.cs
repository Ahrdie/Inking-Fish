﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task
{
    public List<avaliableColors> objectionItems = new List<avaliableColors>{
        avaliableColors.YELLOW,
        avaliableColors.GREEN,
        avaliableColors.BLUE,
        avaliableColors.RED,
        avaliableColors.RED,
        avaliableColors.ORANGE,
        avaliableColors.RED
    };
}

public class ObjectionManager : MonoBehaviour
{
    public GameObject emptyFishPrefab;
    private List<GameObject> fishItems = new List<GameObject>();
    public Sprite filledFishSprite;
    private InkBox inkBox;

    private PlayerController player;

    public List<Task> tasks;
    private Task loadedTask;
    private Task testTask= new Task();

    private int orbsEaten = 0;
    
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        inkBox = FindObjectOfType<InkBox>();
        BuildUpTask(testTask);
    }

    private void BuildUpTask(Task task){
        loadedTask = task;
        float width = emptyFishPrefab.GetComponent<RectTransform>().rect.width;
        int space = 5;
        float joinedWidth = width + space;
        int numberOfTasks = task.objectionItems.Count;
        float anchorLeft = -(numberOfTasks % 2 == 0 ? numberOfTasks/2 * joinedWidth : numberOfTasks/2 * joinedWidth - space/2);

        for (int i = 0; i < numberOfTasks; i++){
            GameObject fishItem = Instantiate(emptyFishPrefab, gameObject.transform);
            fishItems.Add(fishItem);
            fishItem.transform.Translate(new Vector3(anchorLeft + i * (joinedWidth),0.0f,0.0f));
            avaliableColors color = task.objectionItems[i];
            Color newColor = (inkBox.colors[color]);
            newColor.a = 0.45f;
            fishItem.GetComponent<Image>().color = newColor;
        }
    }

    public void EatColor(avaliableColors colorToEat){
        if (colorToEat == loadedTask.objectionItems[orbsEaten])
        {
            fishItems[orbsEaten].GetComponent<Image>().sprite = filledFishSprite;
            orbsEaten++;
        }
    }
}