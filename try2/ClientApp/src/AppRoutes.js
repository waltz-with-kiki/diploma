import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import UserCreator from "./components/UserCreator.jsx";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
    {
        path: '/user-creator',
        element: <UserCreator />
    }
];

export default AppRoutes;
