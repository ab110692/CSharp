package br.com.avancertecnologia.util.task.listeners;

public abstract class TaskListenerAbstract implements  TaskListener {

    @Override
    public abstract void inicio();

    @Override
    public abstract void atualizar(Object o);

    @Override
    public abstract void atualizar(Object o, int porcentagem);

    @Override
    public abstract void termino();
}
