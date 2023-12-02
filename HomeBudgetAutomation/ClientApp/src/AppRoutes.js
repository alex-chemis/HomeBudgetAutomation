import ApiAuthorzationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { ArticleDirectory } from './components/ArticleDirectory';
import { Home } from "./components/Home";
import Ops from './components/OperationDirectory';
import { BalanceSheet } from './components/BalanceSheet';
import { Report } from './components/Report';

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
    requireAuth: true,
    element: <FetchData />
  },
  {
    path: '/article-directory',
    requireAuth: true,
    element: <ArticleDirectory />
  },
  {
    path: '/operation-directory/:id',
    requireAuth: true,
    element: <Ops />
  },
  {
    path: '/balance-sheet',
    requireAuth: true,
    element: <BalanceSheet />
  },
  {
    path: '/report',
    requireAuth: true,
    element: <Report />
  },
  ...ApiAuthorzationRoutes
];

export default AppRoutes;
