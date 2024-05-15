using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlayerGetter pg;
    public Transform cam;

    public void Save(PlayerSaveFile psf)
    {
        psf.position = transform.position;
        psf.rotationEuler = transform.eulerAngles;
        psf.localRotationEulerCamera = cam.localEulerAngles;
        if (pg.InHandBox != null && pg.InHandBox.TryGetComponent(out BoxGameObject bgo))
        {
            BoxSaveFile bsf = new BoxSaveFile();
            bgo.Save(bsf);
            psf.holdItemId = bsf;
        }
        else
        {
            psf.holdItemId = null;
        }
    }

    public void Load(PlayerSaveFile psf)
    {
        transform.position = psf.position;
        transform.eulerAngles = psf.rotationEuler;
        cam.localEulerAngles = psf.localRotationEulerCamera;
        if(psf.holdItemId != null && psf.holdItemId.id != "")
        {
            Item i = ItemsBase.GetItem(psf.holdItemId.id);
            GameObject o = Instantiate(i.BoxesPrefab);
            o.GetComponent<BoxGameObject>().Load(psf.holdItemId, i);
            o.transform.localPosition = new Vector3(0, -0.4f, 0);
            o.transform.localRotation = Quaternion.identity;
            if(o.TryGetComponent(out Rigidbody rig))
            {
                rig.isKinematic = true;
            }
            if(o.TryGetComponent(out BoxCollider bc))
            {
                bc.isTrigger = true;
            }
            pg.PickItem(o.GetComponent<BoxGameObject>());
        }
    }
}
