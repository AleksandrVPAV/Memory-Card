using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreView;
    private SceneController _controller;

    private void Start()
    {
        _controller = FindObjectOfType<SceneController>();
    }

    private void Update()
    {
        _scoreView.text = _controller.Score.ToString();
    }
}
