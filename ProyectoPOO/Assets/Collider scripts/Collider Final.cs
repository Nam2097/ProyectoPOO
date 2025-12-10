using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderFinal : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    if (collision.gameObject.CompareTag("Player")) 
    {
            SceneManager.LoadSceneAsync(3);
    }
    }
}
