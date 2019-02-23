public interface IConsumableItem
{
	bool IsUsable { get; }
	void Use();
	void Use(int option);
}
