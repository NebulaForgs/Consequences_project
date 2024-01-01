using UnityEngine;

public class MouvementPersonnage : MonoBehaviour
{
    public float vitesseDeplacement = 5f;
    public float sensibiliteSouris = 2f;
    public float limiteAngleHaut = 80f; // Limite d'inclinaison vers le haut
    public float limiteAngleBas = 80f;  // Limite d'inclinaison vers le bas
    public float forceSaut = 8f;        // Force du saut
    private bool estAuSol;               // Variable pour vérifier si le personnage est au sol
    private CharacterController characterController;
    private float rotationCameraX = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        estAuSol = true;
    }

    void Update()
    {
        // Mouvements du personnage
        float deplacementAvantArriere = Input.GetAxis("Vertical") * vitesseDeplacement;
        float deplacementGaucheDroite = Input.GetAxis("Horizontal") * vitesseDeplacement;

        Vector3 deplacement = new Vector3(deplacementGaucheDroite, 0, deplacementAvantArriere);
        deplacement = transform.TransformDirection(deplacement);

        // Saut
        if (Input.GetButtonDown("Jump") && estAuSol)
        {
            deplacement.y = forceSaut;
            estAuSol = false;
        }

        characterController.Move(deplacement * Time.deltaTime);

        // Rotation de la caméra avec la souris
        float rotationSourisX = Input.GetAxis("Mouse X") * sensibiliteSouris;
        transform.Rotate(0, rotationSourisX, 0);

        // Rotation de la caméra vers le haut et le bas avec la souris
        rotationCameraX -= Input.GetAxis("Mouse Y") * sensibiliteSouris;
        rotationCameraX = Mathf.Clamp(rotationCameraX, -limiteAngleBas, limiteAngleHaut);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationCameraX, 0, 0);

        // Vérification si le personnage est au sol
        if (characterController.isGrounded)
        {
            estAuSol = true;
        }
        else
        {
            Debug.Log("Le personnage n'est pas au sol.");
        }
    }
}
