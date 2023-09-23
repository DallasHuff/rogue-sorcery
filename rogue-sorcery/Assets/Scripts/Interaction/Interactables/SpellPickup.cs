using UnityEngine;
using Rogue.Spell;
using Rogue.Utilities;

namespace Rogue.Interaction.Interactables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpellPickup : MonoBehaviour, IInteractable<SpellDataSO>
    {
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

        [SerializeField] private SpriteRenderer weaponIcon;
        [SerializeField] private Bobber bobber;

        [SerializeField] private SpellDataSO weaponData;

        public SpellDataSO GetContext() => weaponData;
        public void SetContext(SpellDataSO context)
        {
            weaponData = context;

            weaponIcon.sprite = weaponData.Icon;
        }

        public void Interact()
        {
            Destroy(gameObject);
        }

        public void EnableInteraction()
        {
            bobber.StartBobbing();
        }

        public void DisableInteraction()
        {
            bobber.StopBobbing();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void Awake()
        {
            Rigidbody2D ??= GetComponent<Rigidbody2D>();
            weaponIcon ??= GetComponentInChildren<SpriteRenderer>();

            if (weaponData is null)
                return;

            weaponIcon.sprite = weaponData.Icon;
        }
    }
}