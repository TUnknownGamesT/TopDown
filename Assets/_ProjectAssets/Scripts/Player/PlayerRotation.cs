using UnityEngine;
public class PlayerRotation : MonoBehaviour
{
    public float distanceOfPoint;
    public Transform playerBody;
    public GameObject cube;
    public float  aimOffset;

    public Vector3 mousePosition = Vector3.zero;

    
    void Update()
    {
        if (!UIManager.instance.isPaused)
        {
            RotatePlayer();
        }
    }

    public void Die()
    {
        this.enabled = false;
    }
    private void RotatePlayer()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = distanceOfPoint;
        mousePosition =  Camera.main.ScreenToWorldPoint(mousePosition);


        // Ray ray = Camera.main.ScreenPointToRay(mousePosition = Input.mousePosition);
        //
        // // Raycast to determine the point in 3D space
        // RaycastHit hit;
        // if (Physics.Raycast(ray, out hit))
        // {
        //     // Calculate rotation to look at the mouse position
        //     Vector3 lookAtPoint = new Vector3(hit.point.x, playerBody.position.y, hit.point.z);
        //     cube.transform.position = lookAtPoint;
        //     lookAtPoint.x += aimOffset;
        //     Quaternion rotation = Quaternion.LookRotation(lookAtPoint - playerBody.position);
        //
        //     // Smoothly rotate the player towards the mouse
        //     playerBody.rotation = Quaternion.Slerp(playerBody.rotation, rotation, 100 * Time.deltaTime);
        // }
        
         mousePosition.y = playerBody.position.y;
         cube.transform.position = mousePosition;
         playerBody.LookAt(mousePosition);
    }
}
