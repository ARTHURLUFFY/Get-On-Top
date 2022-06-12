using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    void move(Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene);
        //SceneManager.LoadScene("Game");
        //Destroy(gameObject);
    }

}
