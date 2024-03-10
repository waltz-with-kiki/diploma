
import { Home } from "./components/Home";
import PreparationPage from "./components/PreparationPage.jsx";
import ExperimentPage from "./components/ExperimentPage.jsx";
import ExpertAssessmentPage from "./components/ExpertAssessmentPage.jsx";
import AnalysisPage from "./components/AnalysisPage.jsx";


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/preparation',
    element: <PreparationPage></PreparationPage>
  },
  {
    path: '/experiment',
    element: <ExperimentPage></ExperimentPage>
  },
  {
    path: '/expertAssessment',
    element: <ExpertAssessmentPage></ExpertAssessmentPage>
  },
  {
    path: '/analysis',
    element: <AnalysisPage></AnalysisPage>
  },
  
];

export default AppRoutes;
