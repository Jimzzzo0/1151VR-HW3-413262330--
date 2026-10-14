using UnityEngine;
using UnityEngine.InputSystem; 

public class RouletteController : MonoBehaviour
{
    private float rotSpeed = 0f; 
    private bool isSpinning = false; 


    private Vector3 originalScale; // 用來記錄轉盤「原本在 Inspector 裡的大小」
    private Vector3 targetScale;   // 目標大小
    private Vector3 currentVelocity = Vector3.zero;        
    public float bounceSharpness = 15f;                    

    // 綁定照片物件的變數
    public GameObject badLuckImage; 

    void Start()
    {
 
        originalScale = transform.localScale;
        targetScale = originalScale; // 目標大小就是原始大小

        if (badLuckImage != null) badLuckImage.SetActive(false);
    }

    void Update()
    {
        // 2. 偵測點擊與旋轉
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            this.rotSpeed = Random.Range(2f, 8f); 
            
    
            transform.localScale = originalScale * 0.8f;
            
            isSpinning = true; 

            if (badLuckImage != null) badLuckImage.SetActive(false);
        }

        // 3. 處理旋轉與減速
        transform.Rotate(0, 0, this.rotSpeed);
        this.rotSpeed *= 0.97f;

        // 4. 偵測「剛好完全停止」的瞬間
        if (isSpinning && this.rotSpeed < 0.01f)
        {
            this.rotSpeed = 0f;
            isSpinning = false; 

        
            float finalAngle = transform.eulerAngles.z;

            Debug.Log("目前停下來的角度是: " + finalAngle);

        
            if (finalAngle >= 210f && finalAngle <= 270f)
            {
                
                if (badLuckImage != null)
                {
                    badLuckImage.SetActive(true);
                }
            }

        }


        transform.localScale = Vector3.SmoothDamp(
            transform.localScale, 
            targetScale, 
            ref currentVelocity, 
            1f / bounceSharpness
        );
    }
}
