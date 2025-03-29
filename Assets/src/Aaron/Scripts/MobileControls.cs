using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileControls : MonoBehaviour
{
    public GameObject mobileUI; // Parent object containing joystick + buttons
    public Joystick joystick;
    public Button dashButton;
    public Button meleeButton;
    public GameObject reticle;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool isMobile;

    void Start()
    {
        // Check if we're on mobile
        isMobile = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);
        
        // Enable or disable mobile UI based on platform
        if (mobileUI != null)
        {
            mobileUI.SetActive(isMobile);
        }

        if (isMobile)
        {
            dashButton.onClick.AddListener(OnDashButtonPressed);
            meleeButton.onClick.AddListener(OnMeleeButtonPressed);
        }
    }

    void Update()
    {
        if (isMobile)
        {
            HandleMobileMovement();
            HandleTouchShooting();
        }
    }

    void HandleMobileMovement()
    {
        Vector2 moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();
            GetComponent<PlayerMovement>().moveDirection = moveDirection;
        }
    }

    void HandleTouchShooting()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ShootAtTouchPosition(touch.position);
            }
        }
    }

    void ShootAtTouchPosition(Vector2 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;

        // Move reticle
        reticle.transform.position = worldPosition;

        // Flip player based on target
        Vector2 direction = (worldPosition - transform.position).normalized;
        transform.localScale = new Vector3(direction.x > 0 ? -1 : 1, 1, 1);

        // Fire bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;
    }

    void OnDashButtonPressed()
    {
        GetComponent<PlayerMovement>().Dash();
    }

    void OnMeleeButtonPressed()
    {
        Debug.Log("Melee attack triggered!");
        // Add melee attack logic here
    }
}
