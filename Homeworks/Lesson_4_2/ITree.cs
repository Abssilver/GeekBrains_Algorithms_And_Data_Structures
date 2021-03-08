namespace Lesson_4_2
{
    public interface ITree
    {
        Vertex GetRoot();
        void AddItem(int value); // добавить узел
        void RemoveItem(int value); // удалить узел по значению
        Vertex GetVertexByValue(int value); //получить узел дерева по значению
        void PrintTree(); //вывести дерево в консоль
        void PrintSecondVariant();
    }
}